# Set the output file path
$outputFile = "tree_structure.md"

# Clear the content of the output file if it exists
if (Test-Path $outputFile) {
    Remove-Item $outputFile
}

# Function to generate the tree structure
function Generate-Tree {
    param (
        [string]$Path,
        [string]$Prefix = ""
    )
	$spacePrefix = "$Prefix`&#160;`&#160;`&#160;`&#160;"  # Add four non-compressing spaces for indentation
    # Relevant file extensions to include
    $extensions = @(".cs", ".html", ".razor", ".cshtml")

    # Directories to exclude from traversal
    $excludedDirs = @("bin", "obj", ".git", ".github", ".vs", "node_modules", "packages", "Migrations", "logs", "Properties", "wwwroot", "MVCWasmNuget")

    # Files to exclude
    $excludedFiles = @(".csproj", ".dll", ".exe", ".pdb", ".user", ".log", ".json", ".xml")
#, "Program.cs"

    # Get all directories and relevant files in the current path
    $items = Get-ChildItem -Path $Path -Force | Where-Object {
        ($_.PSIsContainer -and ($_.Name -notin $excludedDirs)) -or
        ($_.Extension -in $extensions -and $_.Name -notin $excludedFiles)
    } | Sort-Object Name

    $outputLines = @() # Initialize an array to hold output lines

    for ($i = 0; $i -lt $items.Count; $i++) {
        $item = $items[$i]

        # Prepare the output string for the item using square brackets
        if ($i -eq $items.Count - 1) {
            # Last item in the sibling group
            $outputString = "$Prefix$spacePrefix&#9495; $($item.Name)"  # Last item
        } else {
            # Other items
            $outputString = "$Prefix$spacePrefix&#9507; $($item.Name)"  # Sibling item
        }

        # Add the output string to the lines array
        $outputLines += $outputString

        # Recur for directories
        if ($item.PSIsContainer) {
           
            $outputLines += Generate-Tree -Path $item.FullName -Prefix $spacePrefix  # Recur and append
        }
    }

    return $outputLines  # Return the generated lines
}
#style width - not tried yet in this may need tweaking
Add-Content -Path $outputFile -Value "
<style>
tr th:nth-child(2), 
tr td:nth-child(2) {
  white-space: nowrap;       /* Prevent wrapping */
  overflow: visible;         /* Allow overflow */
  text-overflow: unset;      /* Remove ellipsis or clipping */
  word-wrap: normal;         /* Prevent word breaks */
  word-break: normal;        /* Prevent breaking within words */
}
</style>"
# Add the top-level entry for MVCWasmNuget
Add-Content -Path $outputFile -Value "# Project Structure" -Encoding UTF8  # Markdown header
Add-Content -Path $outputFile -Value "|  Description  | File Structure |" -Encoding UTF8  # Table header
Add-Content -Path $outputFile -Value "|----------------|-------------|" -Encoding UTF8  # Table separator
Add-Content -Path $outputFile -Value "|   | &#9507; MVCBlazor |" -Encoding UTF8  # Top-level entry with tree symbol

# Generate tree structure excluding the MVCWasmNuget directory
$treeLines = Generate-Tree -Path (Get-Location) -Prefix "" # Indent all contents

# Append lines to the output file, prepending each with | for the table
$treeLinesWithTableFormat = $treeLines | ForEach-Object { "|  | $_ |" }  # Prepend | to each line and add a second pipe for the description
Add-Content -Path $outputFile -Value $treeLinesWithTableFormat -Encoding UTF8

Write-Host "Tree structure has been generated and saved to $outputFile."
