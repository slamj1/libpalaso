// Copyright (c) 2013, SIL International.
// Distributable under the terms of the MIT license (http://opensource.org/licenses/MIT).
using System;
using System.Drawing;

namespace Palaso.UI.WindowsForms.Keyboarding.Interfaces
{
	/// <summary>
	/// Event handler interface for IBus events.
	///
	/// This interface can be implemented by an application that needs specific behavior with
	/// IBus preedit handling. For example FieldWorks implements a handler to allow in-place
	/// edits of IBus in views.
	///
	/// IBus 'keyboards' can actually be more elaborate input methods involving a pop-up window
	/// in which the user makes choices before text is definitely "committed" as typed into the
	/// control. Another variation involves entering some "pre-edit" text actually into the
	/// control which may not be the final version, like an 'a' which might get replaced with a
	/// composite ae if e is the next keystroke. This calls for interaction between a control
	/// and the IBus adapter so that the control can choose where to place the pop-up and to
	/// allow the keyboard to manage the pre-edit text.
	///
	/// If the application implements this interface it will need to pass an instance
	/// when registering the control (<see cref="KeyboardController.Register"/>).
	///
	/// If the application doesn't implement this interface a default event handler
	/// (<see cref="Palaso.UI.WindowsForms.Keyboarding.Linux.IbusDefaultEventHandler"/>) will
	/// be used that works with a WinForms TextBox.
	/// </summary>
	public interface IIbusEventHandler
	{
		#region Methods called by keyboard adapter
		/// <summary>
		/// Called by the IBusKeyboardAdapter to cancel any open compositions, e.g. after the
		/// user pressed the ESC key or if the application loses focus.
		/// </summary>
		void Reset();

		/// <summary>
		/// Called by the IBusKeyboardAdapter to get the position (in pixels) and line height of
		/// the end of the selection. The position is relative to the screen in the
		/// PointToScreen sense, that is (0,0) is the top left of the primary monitor.
		/// IBus will use this information when it opens a pop-up window to present a list of
		/// composition choices.
		/// </summary>
		Rectangle SelectionLocationAndHeight { get; }
		#endregion

		#region IBus events
		/// <summary>
		/// This method gets called when the IBus CommitText event is raised and indicates that
		/// the composition is ending. The temporarily inserted composition string will be
		/// replaced with <paramref name="text"/>.
		/// It's in the discretion of the IBus 'keyboard' to decide when it calls OnCommitText.
		/// Typically this is done when the user selects a string in the pop-up composition
		/// window, or when he types a character that isn't part of the previous composition
		/// sequence.
		/// </summary>
		/// <seealso cref="IBusKeyboardAdaptor.HandleKeyPress"/>
		void OnCommitText(string text);

		/// <summary>
		/// Called when the IBus UpdatePreeditText event is raised to update the composition.
		/// </summary>
		/// <param name="compositionText">New composition string that will replace the existing
		/// composition (sub-)string.</param>
		/// <param name="cursorPos">1-based index in the composition (pre-edit window). The
		/// composition string will be replaced with <paramref name="compositionText"/> starting
		/// at this position.</param>
		/// <seealso cref="IBusKeyboardAdaptor.HandleKeyPress"/>
		void OnUpdatePreeditText(string compositionText, int cursorPos);

		/// <summary>
		/// Called when the IBus HidePreeditText event is raised to cancel/remove the composition,
		/// e.g. after the user pressed the ESC key or the application lost focus. An implementor
		/// will typically call Reset() in the implementation of this event handler method.
		/// </summary>
		void OnHidePreeditText();

		/// <summary>
		/// Called when the IBus ForwardKeyEvent (as in: forwarding key event) is raised, i.e.
		/// when IBus wants the application to process a simulated key event. One example is when
		/// IBus raises this event and passes backspace to delete the character to the left of the
		/// cursor so that it can be replaced with a new modified character.
		/// </summary>
		/// <param name="keySym">Key symbol.</param>
		/// <param name="scanCode">Scan code.</param>
		/// <param name="modifiers">Index into a vector of keycodes associated with a given key
		/// depending on which modifier keys are pressed. 0 is always unmodified, and 1 is with
		/// shift alone.
		/// </param>
		/// <seealso cref="IBusKeyboardAdaptor.HandleKeyPress"/>
		void OnIbusKeyPress(int keySym, int scanCode, int modifiers);
		#endregion
	}
}
