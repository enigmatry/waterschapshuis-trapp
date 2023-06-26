#!/usr/bin/env bash

# fail if any command fails
set -e
# debug log
set -x

# Required nodeJS version
NODE_VERSION=14.17.1
BUILD_GRADLE=android/app/build.gradle

# workaround to override the v8 alias
npm config delete prefix
. ~/.bashrc
nvm install "$NODE_VERSION" -q
nvm alias node14 "$NODE_VERSION"

# go to root of project
cd ../..

# patch parameters using msbuild
msbuild ../build-appcenter.proj

echo "Updating App Version $AppVersion in $BUILD_GRADLE"
sed -i '' 's/__AppVersion__/'$AppVersion'/' $BUILD_GRADLE

# install dependencies
npm i

# run optimized production build
npx ng build --prod

# patch plugins for AndroidX
npx jetify

# copy the web assets to the native projects and updates the native plugins and dependencies based in package.json
npx cap sync android