using System;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Inputs;

namespace RichTextEditorSample;

public partial class CustomRichTextEditor : ContentView
{
    private Random random = new Random();

    public CustomRichTextEditor()
    {
        InitializeComponent();
        InitializeControl();
    }

    private void InitializeControl()
    {
        // Define available font sizes, families, and alignment options
        var fontSizes = new ObservableCollection<string> { "10px", "12px", "14px", "16px", "18px", "20px", "24px", "30px" };
        var fontFamilies = new ObservableCollection<string> { "Arial", "Courier New", "Georgia", "Times New Roman", "Verdana" };
        var alignmentOptions = new ObservableCollection<string> { "Left", "Center", "Right", "Justify" };

        // Set the items source for the ComboBoxes
        fontSizeComboBox.ItemsSource = fontSizes;
        fontFamilyComboBox.ItemsSource = fontFamilies;
        alignmentComboBox.ItemsSource = alignmentOptions;

        // Set up the HTML content for the WebView
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

    // Text formatting methods
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

    private async void OnStrikethroughClicked(object sender, EventArgs e)
    {
        await richEditorWebView.EvaluateJavaScriptAsync("document.execCommand('strikethrough');");
    }

    // Link insertion methods
    private void OnAddLinkClicked(object sender, EventArgs e)
    {
        linkPopup.IsOpen = true;
    }

    private void OnInsertLinkClicked(object sender, EventArgs e)
    {
        var button = sender as SfButton;
        if (button?.Parent is Grid grid && grid.Parent is StackLayout layout)
        {
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

    // Color methods
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

    // Font methods
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

    // Formatting methods
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

    // Table insertion method
    private async void OnInsertTableClicked(object sender, EventArgs e)
    {
        await InsertTableIntoWebView(3, 3);
    }

    private async Task InsertTableIntoWebView(int rows, int cols)
    {
        StringBuilder tableHtml = new StringBuilder();
        tableHtml.Append("<table border='1' style='border-collapse: collapse;'>");
        
        for (int i = 0; i < rows; i++)
        {
            tableHtml.Append("<tr>");
            for (int j = 0; j < cols; j++)
            {
                tableHtml.Append("<td style='width: 50px; height: 20px; padding: 5px;'>&nbsp;</td>");
            }
            tableHtml.Append("</tr>");
        }
        
        tableHtml.Append("</table>");

        string script = $"document.execCommand('insertHTML', false, `{tableHtml}`);";
        await richEditorWebView.EvaluateJavaScriptAsync(script);
    }

    // Media insertion methods
    private async void OnInsertMediaClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "public.image", "public.movie" } },
                        { DevicePlatform.Android, new[] { "image/*", "video/*" } },
                        { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".mov" } },
                        { DevicePlatform.macOS, new[] { "public.image", "public.movie" } }
                    }),
                PickerTitle = "Select media"
            });

            if (result != null)
            {
                await InsertMediaIntoWebView(result.FullPath);
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception
        }
    }

    private async Task InsertMediaIntoWebView(string filePath)
    {
        try
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            string mimeType = GetMimeType(fileExtension);
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string base64Data = Convert.ToBase64String(fileBytes);
            string dataUrl = $"data:{mimeType};base64,{base64Data}";

            string script;
            if (mimeType.StartsWith("image/"))
            {
                script = $"document.execCommand('insertImage', false, '{dataUrl}');";
            }
            else if (mimeType.StartsWith("video/"))
            {
                string videoHtml = $"<video width='320' height='240' controls><source src='{dataUrl}' type='{mimeType}'>Your browser does not support the video tag.</video>";
                script = $"document.execCommand('insertHTML', false, `{videoHtml}`);";
            }
            else
            {
                throw new Exception("Unsupported file type");
            }

            await richEditorWebView.EvaluateJavaScriptAsync(script);
        }
        catch (Exception ex)
        {
            // Handle or log the exception
        }
    }

    private string GetMimeType(string extension)
    {
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".mp4" => "video/mp4",
            ".webm" => "video/webm",
            ".ogg" => "video/ogg",
            _ => "application/octet-stream",
        };
    }
}