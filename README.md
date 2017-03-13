# ec_pong
Eric's junky C# pong clone.

## Getting started
This project assumes you've got `mono`.  You should probably see a doctor.

1. Clone the repo and cd into the project directory.
2. `nuget restore` to install packages.
3. In your editor, run your build.  (TODO: Figure out the command to run from the command line and stick it in the `Makefile`)
4. Run tests with `make test`, or with your sweet editor's "run tests" command.
5. Once built, you can run with `mono pong/obj/Debug/pong.exe` (TODO: this would also go in the `Makefile` were I to figure out the build command that needs to come before it)
