#!/bin/bash

# Check if the correct number of arguments is provided
if [ "$#" -ne 1 ]; then
    echo "Usage: ./initday.sh <day_number>"
    exit 1
fi

# Extract the day number from the arguments
DAY_NUMBER=$1
FOLDER_NAME="Day${DAY_NUMBER}"

# Create the folder
mkdir -p "$FOLDER_NAME"

# Create the required files
touch "$FOLDER_NAME/test-input.txt"
touch "$FOLDER_NAME/input.txt"
touch "$FOLDER_NAME/instructions.txt"

# Create the Day{X}.cs file and add the specified code
CS_FILE="$FOLDER_NAME/Day${DAY_NUMBER}.cs"
cat > "$CS_FILE" <<EOL
namespace AdventOfCode2024;

public static class Day${DAY_NUMBER}
{
    public static int Part1(string[] args)
    {

    }
}
EOL

echo "Initialization complete: Folder and files for Day${DAY_NUMBER} created."
