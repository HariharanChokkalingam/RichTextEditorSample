using System.Collections.ObjectModel;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Inputs;

namespace RichTextEditorSample;

public partial class CustomRichTextEditor : ContentView
{
	public CustomRichTextEditor()
	{
		InitializeComponent();
		InitializeControl();
	}
	private void InitializeControl()
	{
		
              // Define available font sizes and families
            var fontSizes = new ObservableCollection<string> { "10px", "12px", "14px", "16px", "18px", "20px", "24px", "30px" };
            var fontFamilies = new ObservableCollection<string> { "Arial", "Courier New", "Georgia", "Times New Roman", "Verdana" };
             var alignmentOptions = new ObservableCollection<string> { "Left", "Center", "Right", "Justify" };

            fontSizeComboBox.ItemsSource = fontSizes;
            fontFamilyComboBox.ItemsSource = fontFamilies;
            alignmentComboBox.ItemsSource = alignmentOptions;
            // Resources.Add("FontSizes", fontSizes);
            // Resources.Add("FontFamilies", fontFamilies);


            
            // var htmlSource = new HtmlWebViewSource
            // {
            //     Html = @"<html><body contenteditable='true' style='height:100%; overflow:auto;'>Type here...</body></html>"
            // };

            var htmlSource = new HtmlWebViewSource
{
    Html = @"
    <html>
    <head>
        <style>
            body { height:100%; overflow:auto; }
            a { color: blue; cursor: pointer; text-decoration: underline; }
        </style>
        <script>
            document.addEventListener('DOMContentLoaded', function() {
                document.body.contentEditable = 'true';

                document.body.addEventListener('click', function(event) {
                    if (event.target.tagName === 'A') {
                        event.preventDefault();
                        openInNewTab(event.target.href);
                    }
                });
            });

            function openInNewTab(url) {
                var win = window.open(url, '_blank');
                win.focus();
            }
        </script>
    </head>
    <body>
        Type here...
    </body>
    </html>"
};
            richEditorWebView.Source = htmlSource;

	}
	
        private async void OnBoldClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('bold');");
        }

        private async void OnItalicClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('italic');");
        }

        private async void OnUnderlineClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('underline');");
        }


        private async void OnAddLinkClicked(object sender, EventArgs e)
        {
            // var url = await DisplayPromptAsync("Insert Link", "Enter URL:");
            // if (!string.IsNullOrEmpty(url))
            // {
            //     await richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('createLink', false, '{url}');");
            // }
            linkPopup.IsOpen = true;
        }
 private void OnInsertLinkClicked(object sender, EventArgs e)
{
    // Find the parent StackLayout containing the Entries
    var button = sender as SfButton;
    if (button?.Parent is Grid grid && grid.Parent is StackLayout layout)
    {
        // Find the Entries within the StackLayout
        var urlEntry = layout.Children.OfType<VerticalStackLayout>()
                                     .SelectMany(vsl => vsl.Children.OfType<Entry>())
                                     .FirstOrDefault(entry => entry.Placeholder.Contains("http"));
        
        var displayTextEntry = layout.Children.OfType<VerticalStackLayout>()
                                             .SelectMany(vsl => vsl.Children.OfType<Entry>())
                                             .FirstOrDefault(entry => entry.Placeholder.Contains("Display"));

        if (urlEntry != null && displayTextEntry != null)
        {
            var url = urlEntry.Text;
            var displayText = displayTextEntry.Text;

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(displayText))
            {
                richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('insertHTML', false, '<a href=\"{url}\">{displayText}</a>');");
            }
        }

        linkPopup.IsOpen = false;
    }
}

private void OnCancelLinkClick(object sender, EventArgs e)
{
    linkPopup.IsOpen = false;
}

// ... existing code ...

        private Random random = new Random();

        private string GetRandomColor()
        {
        return $"#{random.Next(0x1000000):X6}";
        }

        private async void OnTextColorClicked(object sender, EventArgs e)
        {
        string color = GetRandomColor();
        await richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('foreColor', false, '{color}');");
        }

        private async void OnBackgroundColorClicked(object sender, EventArgs e)
        {
        string color = GetRandomColor();
        await richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('hiliteColor', false, '{color}');");
        }



       
         private async void OnFontSizeChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as SfComboBox).SelectedItem;
            if (selectedItem != null)
            {
                string fontSize = selectedItem.ToString();
                await richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('fontSize', false, '{fontSize}');");
            }
        }

        private async void OnFontFamilyChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as SfComboBox).SelectedItem;

            if (selectedItem != null)
            {
                string fontFamily = selectedItem.ToString();
                await richEditorWebView.EvaluateJavaScriptAsync($"document.execCommand('fontName', false, '{fontFamily}');");
            }
        }

         private async void OnClearFormatClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('removeFormat');");
        }

        private async void OnRedoClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('redo');");
        }

        private async void OnUndoClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('undo');");
        }

        private async void OnStrikethroughClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('strikethrough');");
        }

        private async void OnSubScriptClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('subscript');");
        }

        private async void OnSuperScriptClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('superscript');");
        }

        private async void OnBulletListClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('insertUnorderedList');");
        }

        private async void OnNumberListClicked(object sender, EventArgs e)
        {
            await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('insertOrderedList');");
        }

         private async void OnAlignmentChanged(object sender, EventArgs e)
        {
            var selectedAlignment = (sender as SfComboBox).SelectedItem.ToString();

            switch (selectedAlignment)
            {
                case "Left":
                    await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('justifyLeft');");
                    break;
                case "Center":
                    await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('justifyCenter');");
                    break;
                case "Right":
                    await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('justifyRight');");
                    break;
                case "Justify":
                    await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('justifyFull');");
                    break;
            }
        }
            
        // Add methods for other features as needed
}