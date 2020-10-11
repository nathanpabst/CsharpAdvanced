using System;

namespace CsharpAdvanced
{
    internal class NinjaCoding
    {
        // Ninja Coding
        // _____________TEXT SELECTION_________________
        // Control + right OR left arrow: moves the cursor to the right or left one word at a time
        // Function + home: moves the cursor to the beginning of the line
        // Function + end: moves the cursor to the end of the current line
        // fn + up arrow OR down arrow: scrolls to the next method
        // control + function + end: jump to the end of the file
        // control + function + home: jump to the beginning of the file
        // shift key: to activate selection
        // shift + function + right arrow: select from where the cursor is to the end of the line
        // shift + function + left arrow: select from where the cursor is to the beginning of the line
        // control + c: copies the current line
        // control + v: pastes the line below
        // control + x: removes the current line
        // _____________DELETING TEXT______________________
        // control + delete : removes one word at a time. i.e. the word to the right of the cursor
        // control + backspace : removes one word at a time. i.e. the word to the left of the cursor
        // control + L : deletes the current line
        // _____________COMMENTING CODE______________
        // Select the text, + Control + K, and Control + C
        // De-select comments...Select the text, + control K + control U
        // _____________BOOKMARKING____________________
        // to bookmark a spot && to remove a bookmark from a line: control + K + K
        // to view all bookmarks: control + w + b ...mayyyyyybe??
        // Expanding/Collapsing Code: control + M, control + M
        //_____________WORKING WITH TABS_______________
        // control + T: resharper search hotkey. displays a list of files in the solution
        // control + tab : to cycle through open tabs
        // control + Shift + tab : to reverse the cycle
        // control + tab, tab, etc. allows you to cycle through the list of open tabs
        // control + F6 : navigates to the next open tab
        // control + shift + F6 : navigates back to the previously opened tab
        // control + F4 : to close the current tab
        // to close all open tabs : Alt + W + L
        // control + , : opens the 'navigate to' list of recent files
        // shift + Alt + Enter: to toggle the full screen view.

        public void Method()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("method looping.");
            }
        }
    }
}