#!/usr/bin/env bash

# fail if any command fails
set -e
# debug log
set -x

IOS_PROJECT=ios/App/App.xcodeproj/project.pbxproj
INFO_PLIST=ios/App/App/Info.plist

# Required nodeJS version
NODE_VERSION=14.17.1

# workaround to override the v8 alias
npm config delete prefix
. ~/.bashrc
nvm install "$NODE_VERSION" -q
nvm alias node14 "$NODE_VERSION"

# go to root of project
cd ../..

# patch parameters using msbuild
msbuild ../build-appcenter.proj

echo "Updating App Id $AppId in $IOS_PROJECT"
sed -i '' 's/__appId__/'$AppId'/' $IOS_PROJECT
echo "Updating App Name $AppName in $INFO_PLIST"
sed -i '' 's/__AppName__/'$AppName'/' $INFO_PLIST
echo "Updating App Version $AppVersion in $INFO_PLIST"
sed -i '' 's/__AppVersion__/'$AppVersion'/' $INFO_PLIST

# install dependencies
npm i

# run optimized production build
npx ng build --prod

# copy the web assets to the native projects and updates the native plugins and dependencies based in package.json
npx cap sync ios