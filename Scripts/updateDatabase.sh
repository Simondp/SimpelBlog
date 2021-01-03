#!/bin/bash

if [ -z $1 ]; then
	echo "No migration name, add migration as commandline argument"
else
	export PATH="$HOME/.dotnet/tools:$PATH"
	cd ../Model
	echo "Creating migration"
	 dotnet ef --startup-project ../WebApp migrations add $1
	echo "Done"
	echo "Updating Database"
	dotnet ef --startup-project ../WebApp database update
	echo "Done"
fi


