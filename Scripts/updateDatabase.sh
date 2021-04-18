#!/bin/bash

if [ -z $1 ]; then
	echo "No migration name, add migration as commandline argument"
else
	export PATH="$HOME/.dotnet/tools:$PATH"
	cd ../Model
	echo "Creating migration"
	 dotnet ef --startup-project ../WebApp migrations add $1
	if [ $? -ne 0 ]; then
        echo "Build failed fix project build"
        exit -1
    fi
    echo "Done"
	echo "Updating Database"
	dotnet ef --startup-project ../WebApp database update
	if [ $? -ne 0 ]; then
        echo "Failed to update database"
        exit -1
    fi
	echo "Done"
fi


