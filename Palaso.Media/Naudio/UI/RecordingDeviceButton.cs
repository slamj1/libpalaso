﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Palaso.Media.Naudio.UI
{
	/// <summary>
	/// This 'button' displays an icon and tooltip to indicate which recording device is currently in use.
	/// It also monitors the set of connected RecordingDevices and (a) switches to a new one if it appears,
	/// as for example when a new microphone is plugged in; (b) switches to the default one if the current
	/// one is unplugged.
	/// Enhance JohnT: Possibly the RecordingDeviceButton could respond to a click by cycling through
	/// the available devices, or pop up a chooser...though that is probably overdoing things, users
	/// are unlikely to have more than two. Currently there is no click behavior.
	/// </summary>
	public partial class RecordingDeviceButton : UserControl
	{
		private IAudioRecorder _recorder;

		Timer _checkNewMicTimer = new Timer();

		private HashSet<string> _knownRecordingDevices;

		public RecordingDeviceButton()
		{
			InitializeComponent();
			_checkNewMicTimer.Tick += checkNewMicTimer_Tick;
			_checkNewMicTimer.Interval = 1000;
		}

		public IAudioRecorder Recorder
		{
			get { return _recorder; }
			set
			{
				if (_recorder != null)
					_recorder.SelectedDeviceChanged -= RecorderOnSelectedDeviceChanged;
				_recorder = value;
				if (_recorder != null)
				{
					_recorder.SelectedDeviceChanged += RecorderOnSelectedDeviceChanged;
					_checkNewMicTimer.Start();
					SetKnownRecordingDevices();
				}
				else
				{
					_checkNewMicTimer.Stop();
				}
				if (IsHandleCreated)
					UpdateDisplay();
			}
		}

		private void SetKnownRecordingDevices()
		{
			_knownRecordingDevices = new HashSet<string>(from d in RecordingDevice.Devices select d.ProductName);
		}

		/// <summary>
		/// This is invoked once per second.
		/// It looks for new recording devices, such as when a mic is plugged in. If it finds one,
		/// it switches the Recorder to use it.
		/// It also checks whether the current recording device is still available. If not,
		/// it switches to whatever is the current default recording device (unless a new one was found,
		/// which takes precedence).
		/// The list of RecordingDevice.Devices, at least on Win7 with USB mics, seems to update as things
		/// are connected and disconnected.
		/// I'm not sure this approach will detect the plugging and unplugging of non-USB mics.
		/// </summary>
		/// <remarks>We compare product names rather than actual devices, because it appears that
		/// RecordingDevices.Devices creates a new list of objects each time; the ones from one call
		/// are never equal to the ones from a previous call.</remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void checkNewMicTimer_Tick(object sender, EventArgs e)
		{
			if (_recorder == null)
				return;
			if (_recorder.RecordingState != RecordingState.Monitoring && _recorder.RecordingState != RecordingState.Stopped)
				return;
			bool foundCurrentDevice = false;
			var devices = RecordingDevice.Devices.ToList();
			foreach (var device in devices)
			{
				if (!_knownRecordingDevices.Contains(device.ProductName))
				{
					_recorder.SwitchDevice(device);
					_knownRecordingDevices.Add(device.ProductName);
					UpdateDisplay();
					return;
				}
				if (_recorder.SelectedDevice != null && device.ProductName == _recorder.SelectedDevice.ProductName)
					foundCurrentDevice = true;
			}
			if (!foundCurrentDevice)
			{
				// presumably unplugged...try to switch to another.
				var defaultDevice = devices.FirstOrDefault();
				if (defaultDevice != _recorder.SelectedDevice)
				{
					_recorder.SwitchDevice(defaultDevice);
					UpdateDisplay();
				}
			}
			// Update the list so one that was never active can be made active by unplugging and replugging
			SetKnownRecordingDevices();
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (_checkNewMicTimer != null)
			{
				_checkNewMicTimer.Stop();
				_checkNewMicTimer.Dispose();
				_checkNewMicTimer = null;
			}
			if (_recorder != null)
				_recorder.SelectedDeviceChanged -= RecorderOnSelectedDeviceChanged;
			base.OnHandleDestroyed(e);
		}

		private void RecorderOnSelectedDeviceChanged(object sender, EventArgs eventArgs)
		{
			UpdateDisplay();
		}

		protected override void OnLoad(EventArgs e)
		{
			UpdateDisplay();
			base.OnLoad(e);
		}

		public void UpdateDisplay()
		{
			if (_recorder != null && _recorder.SelectedDevice != null)
			{
				toolTip1.SetToolTip(_recordingDeviceImage, _recorder.SelectedDevice.Capabilities.ProductName);
			}
			else
			{
				toolTip1.SetToolTip(_recordingDeviceImage, "no input device");
			}
			if (_recorder == null)
				return;

			// It's rather arbitrary which one we use if we have no recording device.
			// A microphone seems most likely to suggest what needs to be connected.
			if (Recorder.SelectedDevice == null)
				_recordingDeviceImage.Image = AudioDeviceIcons.Microphone;
			else if(_recorder.SelectedDevice.GenericName.Contains("Internal"))
				_recordingDeviceImage.Image = AudioDeviceIcons.Computer;
			else if (_recorder.SelectedDevice.GenericName.Contains("USB Audio Device"))
				_recordingDeviceImage.Image = AudioDeviceIcons.HeadSet;
			else if (_recorder.SelectedDevice.GenericName.Contains("Microphone"))
				_recordingDeviceImage.Image = AudioDeviceIcons.Microphone;

			if (Recorder.SelectedDevice != null)
			{
				var deviceName = _recorder.SelectedDevice.ProductName;

				if (deviceName.Contains("ZOOM"))
					_recordingDeviceImage.Image = AudioDeviceIcons.Recorder;
				else if (deviceName.Contains("Plantronics") || deviceName.Contains("Andrea"))
					_recordingDeviceImage.Image = AudioDeviceIcons.HeadSet;
				else if (deviceName.Contains("Line"))
					_recordingDeviceImage.Image = AudioDeviceIcons.ExternalAudioDevice;
			}

			// REVIEW: For some reason, the icons used to represent the different devices are all different sizes
			// and proportions. Best approach seems to be to scale them down to fit but not scale them up
			// because they will pixelate. It would probably be better to get somebody with an eye for design to
			// come up with consistent looking icons that are of the same size and scale nicely.
			_recordingDeviceImage.SizeMode =
				(_recordingDeviceImage.Image.Height > Height || _recordingDeviceImage.Image.Width > Width) ?
				PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
		}
	}
}
