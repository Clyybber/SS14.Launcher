using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace SS14.Launcher.Views
{
    [GenerateTypedNameReferences]
    public partial class DirectConnectDialog : Window
    {
        private readonly TextBox _addressBox;

        public DirectConnectDialog()
        {
            InitializeComponent();

            _addressBox = AddressBox;
            _addressBox.KeyDown += (_, args) =>
            {
                if (args.Key == Key.Enter)
                {
                    TrySubmit();
                }
            };

            SubmitButton.Command = ReactiveCommand.Create(TrySubmit);

            this.WhenAnyValue(x => x._addressBox.Text)
                .Select(IsAddressValid)
                .Subscribe(b =>
                {
                    InvalidLabel.IsVisible = !b;
                    SubmitButton.IsEnabled = b;
                });
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            _addressBox.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close(null);
            }

            base.OnKeyDown(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void TrySubmit()
        {
            if (!IsAddressValid(_addressBox.Text))
            {
                return;
            }

            Close(_addressBox.Text.Trim());
        }

        internal static bool IsAddressValid(string address)
        {
            return !string.IsNullOrWhiteSpace(address) && UriHelper.TryParseSs14Uri(address, out _);
        }
    }
}
