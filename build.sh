#!/bin/sh
xbuild_command_name="xbuild"

type "$xbuild_command_name" > /dev/null 2>&1
if [ $? != 0 ] ; then
  echo 'Cannot find xbuild.' 1>&2
  exit $?
fi

export EnableNuGetPackageRestore=true

target="$1"
if [ "$target" = "" ] ; then
	target="Build"
fi

xbuild /p:Configuration=Debug /p:TargetFrameworkProfile= /p:OutPutPath=../build/mono/Debug/ /p:BaseIntermediateOutputPath=../build/mono/obj/ /t:"$target" /v:minimal ./OBehave35/OBehave35.csproj
if [ $? != 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Release /p:TargetFrameworkProfile= /p:OutPutPath=../build/mono/Release/ /p:BaseIntermediateOutputPath=../build/mono/obj/ /t:"$target" /v:minimal ./OBehave35/OBehave35.csproj
if [ $? != 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Debug /p:TargetFrameworkProfile= /p:OutPutPath=../build/mono-tests/Debug/ /p:BaseIntermediateOutputPath=../build/mono-tests/obj/ /t:"$target" /v:minimal ./OBehave.Tests/OBehave.Tests.csproj
if [ $? != 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Release /p:TargetFrameworkProfile= /p:OutPutPath=../build/mono-tests/Release/ /p:BaseIntermediateOutputPath=../build/mono-tests/obj/ /t:"$target" /v:minimal ./OBehave.Tests/OBehave.Tests.csproj
if [ $? != 0 ] ; then
    exit $?
fi

exit 0