using System.Windows.Forms;

namespace Palaso.UI.WindowsForms.WritingSystems
{
	public partial class WSPropertiesTabControl : UserControl
	{
		private WritingSystemSetupModel _model;

		public WSPropertiesTabControl()
		{
			InitializeComponent();
		}

		public void BindToModel(WritingSystemSetupModel model)
		{
			_model = model;
			_identifiersControl.BindToModel(_model);
			_fontControl.BindToModel(_model);
			_keyboardControl.BindToModel(_model);
			_sortControl.BindToModel(_model);
			_spellingControl.BindToModel(_model);
		}
	}
}
