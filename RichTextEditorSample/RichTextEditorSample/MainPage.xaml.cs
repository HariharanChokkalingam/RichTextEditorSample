using System.Collections.ObjectModel;
using Syncfusion.Maui.Inputs;
#if WINDOWS
using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Text;
#elif ANDROID
using Android.Widget;
#elif MACCATALYST || IOS
using UIKit;

#endif

namespace RichTextEditorSample;
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


 
//     private void SfButton_Clicked(object sender, EventArgs e)
//     {
// #if WINDOWS
//           var nativeEditor = editor.Handler?.PlatformView as TextBox;
//         if (nativeEditor != null)
//         {

//             //  nativeEditor.Document.Selection.CharacterFormat = new TextCharacterFormat
//             //     {
//             //         Bold = Windows.UI.Text.FormatEffect.On
//             //     };

//             //nativeEditor.SelectedText

//             // var start = nativeEditor.SelectionStart;
//             // var length = nativeEditor.SelectionLength;

//             // if (length > 0)
//             // {
//             //     var text = nativeEditor.Text;
//             //     var selectedText = text.Substring(start, length);
//             //     var boldText = $"*{selectedText}*";

//             //     nativeEditor.Text = text.Remove(start, length).Insert(start, boldText);
//             //     nativeEditor.SelectionStart = start;
//             //     nativeEditor.SelectionLength = boldText.Length;
//             // }
//         }

// #elif ANDROID
//         var nativeEditor = editor.Handler?.PlatformView as EditText;
//         if (nativeEditor != null)
//         {
//             //nativeEditor.span
//         }
// #elif MACCATALYST || IOS
//         var nativeEditor = editor.Handler?.PlatformView as UITextView;
//         if (nativeEditor != null) 
//         { 
//            // nativeEditor.selectedte
//         }


// #endif
//     }

}

// #if WINDOWS
//      public class CustomEditorHandler : EditorHandler
//     {
//        protected override FrameworkElement CreatePlatformView()
//         {
//             var textBox = new TextBox();
//             // To access the native Document API for rich text features
// #if WINDOWS
//             textBox.Document.Selection.CharacterFormat = new TextCharacterFormat
//                 {
//                     Bold = Windows.UI.Text.FormatEffect.On
//                 };
// #endif
//             return textBox;
//         }
//         private void OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
//         {
//             var selectionStart = sender.SelectionStart;
//             var selectionLength = sender.SelectionLength;
            
//             if (selectionLength > 0)
//             {
//                 var boldText = new FontWeight { Weight = FontWeights.Bold.Weight };
//                 sender.Document.Selection.CharacterFormat.Bold = FormatEffect.On;

//                 // Apply bold formatting to selected text
//                 sender.Document.Selection.CharacterFormat = new ITextCharacterFormat
//                 {
//                     Bold = FormatEffect.On
//                 };
//             }
//         }
//     }
// #endif




