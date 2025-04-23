using System;
using Microsoft.Maui.Controls;

namespace ListView_Euroopa_riigid
{
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string noteText = TextEditor.Text;
            if (!string.IsNullOrWhiteSpace(noteText))
            {
                DisplayAlert("Note Saved", "Your note has been saved successfully!", "OK");
            }
            else
            {
                DisplayAlert("Warning", "Please enter a note before saving.", "OK");
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            TextEditor.Text = string.Empty;
            DisplayAlert("Note Deleted", "Your note has been deleted.", "OK");
        }
    }
}