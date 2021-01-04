using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ThesisProject.Internal.Interfaces;

namespace ThesisProject.Internal.Containers
{
    internal abstract class BaseContainer<TViewModel>
        where TViewModel : IComparable<TViewModel>
    {
        private UIElementCollection UIElements { get; set; }
        private List<(Button button, TViewModel viewModel)> ButtonViewModels { get; set; }

        protected event EventHandler<(TViewModel viewModel, string header)> ContextOptionSelected;
        protected event EventHandler<TViewModel> LeftClick;
        protected event EventHandler<TViewModel> RightClick;

        protected BaseContainer()
        {
            ButtonViewModels = new List<(Button button, TViewModel viewModel)>();
        }

        public void Initialize(UIElementCollection uiElements)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public void Show(IEnumerable<TViewModel> viewModels)
        {
            ButtonViewModels.RemoveAll(deviceButton => !viewModels.Contains(deviceButton.viewModel));

            var newDevices = viewModels.Except(ButtonViewModels.Select(deviceButton => deviceButton.viewModel));

            ButtonViewModels.AddRange(newDevices.Select(newDevice => (MakeButton(newDevice), newDevice)));
            ButtonViewModels = ButtonViewModels.OrderBy(deviceButton => deviceButton.viewModel).ToList();

            SetUIElements(ButtonViewModels);
        }

        private TViewModel GetSelectedViewModel(Button selectedButton)
        {
            return ButtonViewModels.First(buttonPath => buttonPath.button == selectedButton).viewModel;
        }

        protected void AddContextOptionIfNotExists(string header, Func<string, bool> conditionByContent)
        {
            var menuItem = new MenuItem();
            menuItem.Header = header;
            menuItem.Click += OnContextOptionSelected;

            var fittingConditionButtons = ButtonViewModels
                .Where(buttonViewModel => conditionByContent(buttonViewModel.button.Content.ToString()))
                .Select(buttonViewModel => buttonViewModel.button);

            foreach (var button in fittingConditionButtons)
            {
                if (button.ContextMenu is null)
                {
                    button.ContextMenu = new ContextMenu();
                }

                if (button.ContextMenu.Items
                    .Cast<MenuItem>()
                    .Any(existingItem => existingItem.Header == menuItem.Header))
                {
                    return;
                }

                button.ContextMenu.Items.Add(menuItem);
            }
        }

        private void OnContextOptionSelected(object sender, RoutedEventArgs e)
        {
            var contextOption = sender as MenuItem;
            var contextMenu = contextOption.Parent as ContextMenu;
            var button = contextMenu.Parent as Button;
            var selectedViewModel = GetSelectedViewModel(button);

            var header = contextOption.Header.ToString();
            ContextOptionSelected(this, (selectedViewModel, header));
        }

        private void SetUIElements(List<(Button button, TViewModel viewModel)> buttonViewModels)
        {
            UIElements.Clear();
            buttonViewModels.ToList().ForEach(deviceButton => UIElements.Add(deviceButton.button));
        }

        private Button MakeButton(TViewModel viewModel)
        {
            var viewModelButton = new Button
            {
                Content = viewModel.ToString(),
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 5),
            };

            viewModelButton.Click += OnLeftButtonClick;
            viewModelButton.PreviewMouseRightButtonUp += OnRightButtonClick;

            return viewModelButton;
        }

        private void OnRightButtonClick(object sender, MouseButtonEventArgs e)
        {
            var selectedViewModel = GetSelectedViewModel(sender as Button);
            RightClick(this, selectedViewModel);
        }

        private void OnLeftButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedViewModel = GetSelectedViewModel(sender as Button);
            LeftClick(this, selectedViewModel);
        }
    }
}
