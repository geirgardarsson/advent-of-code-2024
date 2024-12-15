#!/bin/bash

# Check if the correct number of arguments is provided
if [ "$#" -ne 1 ]; then
    echo "Usage: ./initday.sh <day_number>"
    exit 1
fi

# Extract the day number from the arguments
DAY_NUMBER=$1
FOLDER_NAME="AdventOfCode/Days/Day${DAY_NUMBER}"

# Create the folder
mkdir -p "$FOLDER_NAME"

# Create the required files
touch "$FOLDER_NAME/test-input.txt"
touch "$FOLDER_NAME/input.txt"
touch "$FOLDER_NAME/instructions.txt"

mkdir "$FOLDER_NAME/debug"

# Create the Day{X}.cs file and add the specified code
CS_FILE="$FOLDER_NAME/Day${DAY_NUMBER}.cs"
cat > "$CS_FILE" <<EOL
namespace AdventOfCode2024;

public static class Day${DAY_NUMBER}
{
    public static int Part1(string[] args)
    {
        throw new NotImplementedException();
    }

    public static int Part2(string[] args)
    {
        throw new NotImplementedException();
    }
}
EOL

DAY_UTILS_PATH="AdventOfCode/Utils/DayUtils.cs"

# Check if DayUtils.cs exists and modify it
if [ -f "$DAY_UTILS_PATH" ]; then
    # Prepare the lines to add
    NEW_LINES="        (${DAY_NUMBER}, 1) => Day${DAY_NUMBER}.Part1(args),
        (${DAY_NUMBER}, 2) => Day${DAY_NUMBER}.Part2(args),"
    
    # Insert the new lines above the exception line
    sed -i "/_ => throw new ArgumentOutOfRangeException()/i\\$NEW_LINES" "$DAY_UTILS_PATH"

    echo "Updated DayUtils.cs with Day${DAY_NUMBER} entries."
else
    echo "Error: Could not find $DAY_UTILS_PATH. Skipping DayUtils.cs update."
fi

echo "Initialization complete: Folder and files for Day${DAY_NUMBER} created."
