using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SS14.Launcher.ViewModels.Login;

namespace SS14.Launcher.Views.Login
{
    public sealed class ResendConfirmationView : UserControl
    {
        public ResendConfirmationView()
        {
            InitializeComponent();

            var emailBox = this.FindControl<TextBox>("EmailBox");

            emailBox.KeyDown += InputBoxOnKeyDown;
        }

        private void InputBoxOnKeyDown(object? sender, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && DataContext is ResendConfirmationViewModel vm)
            {
                vm.SubmitPressed();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
