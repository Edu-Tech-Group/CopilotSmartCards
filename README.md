# Prompt-o-mat

This repository contains the source code for the Prompt-o-mat, as seen at events such as SETT Mechelen 2025, and more.

# Release binaries

You can find pre-build binaries in the [latest release](https://github.com/Edu-Tech-Group/Prompt-o-mat/releases/latest).

To install the app on your computer:
- download the latest release
- unzip and open the folder
- find the `Install.ps1` script inside of the `Package_1.0.0.0_Test` folder.
- right-click this file and select "Run with PowerShell"

Once this is finished, you can open the app called "Prompt-o-mat" on your computer.

# Setting up

In order to generate artifacts for this program, you'll need a Code Signing certificate. One can easily be created with the `GenerateCertificate.ps1` PowerShell script.
Run it in the root of this codebase, and update the `Package.appxmanifest` file to point to the newly generated certificate.

# Building and running

To build and run this program, open the solution in Visual Studio.

Make sure the `Package` project is set as startup project, and that you select `x64` as architecture.

Then just hit "F5" to build and run the program.


---

> Please note:
>
> Edu-Tech does not provide any support for this project.


---

### Creating a release

To create a new release with the proper binaries attached, simply push a new tag with the following format: `v[year].[release nr]` - e.g.: `v2025.1`.

The GitHub Actions will do the rest.
