﻿using System;
using System.Reflection.Metadata;

namespace CsharpAdvanced
{
    internal class NinjaCoding
    {
        // Ninja Coding
        // _____________TEXT SELECTION_________________
        // Control + right OR left arrow: moves the cursor to the right or left one word at a time
        // Fn  + home: moves the cursor to the beginning of the line
        // Fn  + end: moves the cursor to the end of the current line
        // fn + up arrow OR down arrow: scrolls to the next method
        // Control + Fn  + end: jump to the end of the file
        // Control + Fn  + home: jump to the beginning of the file
        // Shift  key: to activate selection
        // Shift  + Fn  + right arrow: select from where the cursor is to the end of the line
        // Shift  + Fn  + left arrow: select from where the cursor is to the beginning of the line
        // Control + c: copies the current line
        // Control + v: pastes the line below
        // Control + x: removes the current line
        // Shift + Alt + Down arrow: To select and manipulate text and lines
        // Shift + Windows Key + Home : moves the current program to the upper or lower monitor
        // _____________DELETING TEXT______________________
        // Control + delete : removes one word at a time. i.e. the word to the right of the cursor
        // Control + backspace : removes one word at a time. i.e. the word to the left of the cursor
        // Control + L : deletes the current line
        // _____________COMMENTING CODE______________
        // Select the text, +  Control + K, and  Control + C
        // De-select comments...Select the text, + ControlK + ControlU
        // _____________BOOKMARKING____________________
        // to bookmark a spot && to remove a bookmark from a line: Control + K + K
        // to view all bookmarks: Control + w + b ...mayyyyyybe??
        // Expanding/Collapsing Code: Control + M, Control + M
        //_____________WORKING WITH TABS_______________
        // Control + T: resharper search hotkey. displays a list of files in the solution
        // Control + tab : to cycle through open tabs
        // Control + Shift + tab : to reverse the cycle
        // Control + tab, tab, etc. allows you to cycle through the list of open tabs
        // Control + F6 : navigates to the next open tab
        // Control + Shift  + F6 : navigates back to the previously opened tab
        // Control + F4 : to close the current tab
        // Alt + W + L : to close all open tabs
        // Control + , : opens the 'navigate to' list of recent files
        // Shift  + Alt + Enter: to toggle the full screen view.
        // _____________FIND & REPLACE____________________
        // Control + F: opens the 'Find' dialog box
        // F3 : navigates to the next occurrence of the search term
        // Shift + F3 : navigates to the previous occurrence
        // Control + H : to display the 'replace' dialog
        // Alt + A : to replace all occurrences of the search text
        // Alt + R : to replace the currently selected occurrence
        // Control + Shift + F : search for all instances of a word in a document, project, or solution
        //_____________RUNNING & DEBUGGING__________________
        // Control + F5 : compiles and runs the application. Faster and more responsive than using dubug mode i.e. 'green start button'
        // F5 : Runs the application in Debug Mode (equivalent to pressing the start button on the toolbar)
        // The green 'start' button runs the application in Debug Mode. i.e. VS will load additional information in memory to allow for debugging activities
        // ...using the green start button will also slow down the PC
        // F9 : inserts, or removes, a breakpoint on the current line
        // F10 : Step Over a method while in debug mode
        // F11 : Step Into a method while in debug mode
        // Shift + F5 : stop the debugging session
        // Control + Shift + F5 : Stops the debug session and starts a new sesh
        // Shift + F11 : Step Out of a method (getting out of a method that you accidentally stepped into)
        //_____________COMPILING THE APPLICATION_______________
        // Alt + tab, tab, etc. : view a list of open applications and switch screens
        // Control + Shift + B : Build the application without running/opening a new window
        // Control + R : Refresh the window
        // F8 : To cycle through the errors and warnings. F8 will Navigate to the location of each error in the list. Pressing F8 again will navigate to the next location
        //_____________CODE SNIPPETS_______________
        // ctor + Tab : generates a constructor
        // prop + Tab : generates a property. Press Tab to move to the next parameter.
        // propfull : creates a private field and a public property with full get & set methods
        // cw + Tab : generates 'Console.WriteLine();'
        // equals + Tab : implements the Equals & GetHashCode Methods
        // try + Tab : generates a Try/Catch block
        // tryf + Tab : generates a Try/Finally block
        // for + Tab : generates for loop that increments
        // forr + Tab : generates a for loop that decrements

        public void Method()
        {
            for (int i = length - 1; i >= 0; i--)
            {
            }
            for (int i = 0; i < 0; i++)
            {
            }
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("method looping.");
            }
        }
    }
}