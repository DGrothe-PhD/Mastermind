<link rel="stylesheet" href="docstyles.css">

# System requirements
* Windows 11
* Windows 10 1607+
* Windows 8.1
* Windows 7 SP1
* Works on both x86 and x64 architecture
* .NET 6 runtime required, can be easily installed from the link in the message box

# Installation
Mastermind is portable, no installation is required. To start the game, unpack the zip and double-click the .exe file.

## Game instructions

The inspiration for this came from a board game sold in the last century that we enjoyed playing in the family.\
The computer determines a word, which the user is then allowed to guess.\
To do this, the user clicks on a letter field and selects the desired letter.

Only capital letters from A-Z are allowed, no umlauts.\
The word length can be left at four (as in the board game) or set to three or five.

To end a game or to start a new game, click anywhere in the open game window with the right mouse button. \
Then select the appropriate menu item from the context menu strip.

When entering the letters via the keyboard button, all free fields are filled one by one from left to right.
If you already filled one or two of the fields, these will be skipped. \
So you can put a letter in advance e.g. on the third field and still think about the other fields.

By clicking on a single field, a single letter can be placed or overwritten.
Typing of letters and backspace also works as expected.

### Evaluation
Clicking the OK button starts the evaluation of the input, if all letters are set.

Now zero to (three, four, or five - depending on the word length) pins appear, which indicate a matching letter by their color.

### Example:

| Vorgang            | Wort | Pins |
|:-------------------|------|------|
| Computer chooses:  | THIS |      |
| User tries:        | WINE | ⚪   |
| User:              | PENS | ⚫   |
| User:              | THEM | ⚫⚫ |

etc.