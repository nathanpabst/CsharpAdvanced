using System;
using System.Collections.Generic;
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
        // Shift key: to activate selection
        // Shift + Fn  + right arrow: select from where the cursor is to the end of the line
        // Shift  + Fn  + left arrow: select from where the cursor is to the beginning of the line
        // Control + c (anywhere within the line): copies the current line
        // Control + v: pastes the line below
        // Control + x: removes the current line
        // Shift + Alt + Down arrow: To select and manipulate text and lines
        // Shift + Windows Key + Home : moves the current program to the upper or lower monitor
        // Windows key + right arrow : moves the current application to the right side of the screen
        // Windows key + up arrow : expands the application/program to full size
        // _____________DELETING TEXT______________________
        // Control + delete : removes one word at a time. i.e. the word to the right of the cursor
        // Control + backspace : removes one word at a time. i.e. the word to the left of the cursor
        // Control + L OR Shift + Delete: deletes the current line
        // Control + Z : undo the previous command
        // _____________COMMENTING CODE______________
        // Select the text OR anywhere on the line, then Control K, and Control C
        // De-select comments...Select the text + Control K and Control U
        // _____________BOOKMARKING____________________
        // to bookmark a spot && to remove a bookmark from a line: Control + K + K
        // to view all bookmarks: Control + w + b ...mayyyyyybe?? mmm, no
        // Expanding/Collapsing Code blocks: Control + M, Control + M
        //_____________WORKING WITH TABS_______________
        // Control + T: resharper search hotkey. displays a list of files in the solution
        // Control + Tab : to cycle through open Tabs
        // Control + Shift + Tab : to reverse the cycle
        // Control + Tab, Tab, etc. allows you to cycle through the list of open Tabs
        // Control + Tab + left arrow: go to additional project options (like Team Explorer, Error List, etc.)
        // Control + F6 : navigates to the next open Tab
        // Control + Shift  + F6 : navigates back to the previously opened Tab
        // Control + F4 : to close the current Tab
        // Alt + W + L : to close all open Tabs
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
        // Shift + Esc : closes the Error List, Task List, etc
        //_____________COMPILING THE APPLICATION_______________
        // Alt + Tab, Tab, etc. : view a list of open applications and switch screens
        // Control + Shift + B : Builds the application without running/opening a new window
        // Control + R : Refresh the window
        // F8 : To cycle through the errors and warnings. F8 will Navigate to the location of each error in the list. Pressing F8 again will navigate to the next location
        //_____________CODE SNIPPETS_______________
        // ctor + Tab : generates a constructor
        // prop + Tab : generates a property. Press Tab to move to the next parameter.
        // propfull : creates a private field and a public property with full get & set methods
        // cw + Tab : generates 'Console.WriteLine();'
        // equals + Tab : to override the equals method of the object class. Implements the Equals & GetHashCode Methods
        // try + Tab : generates a Try/Catch block
        // tryf + Tab : generates a Try/Finally block
        // for + Tab : generates for loop that increments
        // forr + Tab : generates a for loop that decrements
        // foreach + Tab : when placing the foreach block near the variable...
        // ...VS will detect/suggest the collection so you don't have to manually specify the loop variable
        // while + tab : to create a while loop
        // do + tab : to create a do loop
        //___________COMMITTING CHANGES______________________
        // Control + Q : activates the search window
        //...type & select Team Explorer
        //...Tab to get to the Changes menu
        //___________FORMATTING CODE______________________
        // select the code using Shift + direction arrow, press the Tab key to move/indent the code one Tab stop to the right
        // select the code using Shift + direction arrow, press Shift + Tab key to move/indent the code one Tab stop to the left
        // Control + A to select the entire file then Control K + Control F : to format the entire file
        // GET VISUAL STUDIO EXTENSION: PRODUCTIVITY POWER TOOLS (VIA GOOGLE)
        //https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.ProductivityPowerPack2017
        //___________RESHARPER_____________________________
        //  www.jetbrains.com/resharper --free for non-commercial use
        // Keyboard Schemes: select the Visual Studio Shortcuts Scheme
        // ...Extensions --> Resharper --> Options : to select a scheme:
        // ...Alt + R : to quickly navigate to the Resharper menu
        // Alt + R --> Options --> Environment --> Keyboards & Menus
        //____________Camel Humps__________________________
        // Adding the LastIndexOf method : start typing the name of the variable, select the variable, add a period, type 'lio' and select/press enter
        // Adding the ArgumentNullException : type 'throw new + ANE'
        // _____________SUGGESTING NAMES________________
        // Control + Spacebar : Suggesting names for Properties or Fields. The first two options are properties. The second two options are field name suggestions
        //...or type Shift + _ (underscore) to see suggested field names
        // _____________INITIALIZING PRIVATE FIELDS________________
        // Alt + Enter : To activate Resharpers commands/suggestions
        //Steps to initialize a private field from the constructor...
        //1. ctor + Tab
        //2. CI (this represents an existing class)+ Enter in the parens : Defining an argument for the constructor using camel humps
        //3. Control + Spacebar to get a list of suggested names for the argument
        //4. highlight the selected name #squigglybois then press Alt + Enter
        //5. select the 'Introduce and initialize field 'blahblah' + enter
        //_____________AUTO-IMPORTING NAMESPACES___________________
        // Control + Spacebar : activates auto-completion
        // Alt + Enter
        // Alt + Spacebar : opens windows options for...restoring, minimizing, maximizing, closing, etc.
        //_____________NAVIGATION___________________
        // Control + Shift + T : opens the Go-To File window to search for a specific file or folder name
        // Shift + Alt + L : opens the Solution Explorer. use up/down arrows to browse and press Enter to select
        // Control + T : to bring up the Go to File window & search by type. for example the Divide Method in the Calculator class.
        // Alt + down arrow : navigates to the next member/method
        // Alt + up arrow : navigates to the previous member/method
        // Alt + backslash key (above the Enter Key) : to search all the members (methods and properties) in a file. use arrows to navigate and Enter to select
        //____________CODE SELECTION________________
        // Control + W : hotkey for selecting code. to continue selecting code, press and hold the Control button and press W until you have everything you want.
        //____________CREATING LIVE TEMPLATES aka CODE SNIPPETS_________
        //1. Control + W : to select the block of code you wish to duplicate
        //2. Alt + R : to navigate to the Resharper menu
        //3. Select Tools --> Create Live Template from Selection --> Create & Edit
        //4. Modify the snippet as needed
        //5. Define the name of the shortcut
        //6. Control + S to save
        //7. Type the name of the shortcut anywhere in the project to use the snippet
        // ___To view a list of all templates/code snippets...
        //1. Alt + R : to navigate to the Resharper menu
        //2. Select Tools --> Templates Explorer + Enter key
        //3. Code snippets will likely be under C# in the Scopes menu
        // ___To Edit a template...
        //1. Alt + R : to navigate to the Resharper menu
        //2. Select Tools --> Templates Explorer + Enter key
        //3. Select the Code snippet & click on the pencil icon
        //4. Make the snippet dynamic by surrounding each part or parameter (method name, condition, expectation) with the '$' key
        //5. Example: public async Task $MethodName$_$ConditionToBeTested$_$Expectation$
        //6. Control + S to save and then close

        public void Method()
        {
        }
    }
}