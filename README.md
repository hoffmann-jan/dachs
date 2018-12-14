![dachs Icon](badger.svg)

# dachs
> dynamic address components help system

gets all house numbers of streets of Leipzig

a console application and  Excel AddIn running on .NET Core

## Features
dachs determines all house numbers of a given street or the house numbers of all streets in Leipzig
* the console application returns this data on the console, as a csv file or Excel file
* the Excel AddIn provides an Excel function with which this data can be retrieved directly in Excel

## Requirements
To run dachs, you'll need to install [.Net Core Runtime v2.1](https://github.com/dotnet/core-setup) or greater.

## Console Application

### Getting started
1. Open a command prompt or a terminal
2. Navigate to the folder `Console Application`
3. Type in `dotnet dachs.dll` to start the dachs console app

## Excel AddIn
### Installation
1. Open Excel
2. Go to `file` > `options`
3. Switch to the tab `Add-Ins`
4. Hit the `Go` button at the bottom
5. Hit `Browse` button
6. Select the `dachs.xll` file
7. Confirm all windows with `ok`

### Getting started
1. Edit a cell
2. Type in `=Hausnummern(<cellNumber>)` or `=Hausnummern("<streetName>")` to get all available house numbers
or type in `=AnzahlHausnummern(<cellNumber>)`  or `=AnzahlHausnummern("<streetName>")` to count all house numbers of a given street

## Used external libraries
* [HtmlAgilityPack](https://github.com/zzzprojects/html-agility-pack), Version 1.8.11 (MIT License)
* [EPPlus](https://github.com/JanKallman/EPPlus), Version 4.5.2.1 (GNU Lesser General Public License v3.0)
* [Excel-DNA](https://github.com/Excel-DNA/ExcelDna), Version 0.34.6 (zlib License)
* [ExcelDna.AddIn](https://github.com/Excel-DNA/AddInManager), Version 0.34.6 (MIT License)
* [ExcelDna.Integration](https://github.com/Excel-DNA/ExcelDna/tree/master/Source/ExcelDna.Integration), Version 0.34.6 (zlib License)

## Licensing
The code of this project is licensed under GNU General Public License v3.0.
