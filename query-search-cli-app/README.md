# Query Search CLI App

A command-line application that searches for a user-provided query within a specified text file.

## Acceptance criteria

### CLI features

- The user will provide the query and the path as command-line arguments
- The application should print the list of lines from the file that contain the query

### Core features

- Should return each line containing the query
- Should return no line when there is no match for the query
- The query should be case-insensitive
- The line number should be provided with the search result
- The text file should be read from the disk

### Error handling

- The app should fail gracefully when not enough CLI arguments are provided
  - Error message: `Parsing arguments error: not enough arguments specified`
- The app should fail gracefully when the file does not exist
  - Error message: `Application error: The system cannot find the file specified`
