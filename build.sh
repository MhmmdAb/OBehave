#!/bin/sh
xbuild_command_name="xbuild"

if [type "$xbuild_command_name" > /dev/null]; then
  echo 'Cannot find xbuild.' 1>&2
  exit 1
fi

target="$1"
if [ "$target" = "" ] ; then
	target="Build"
fi

xbuild /p:Configuration=Debug /p:OutPutPath=../build/mono/Debug/ /t:"$target" /v:minimal ./OBehave35/OBehave35.csproj
if [ $? = 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Release /p:OutPutPath=../build/mono/Release/ /t:"$target" /v:minimal ./OBehave35/OBehave35.csproj
if [ $? = 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Debug /p:OutPutPath=../build/mono-tests/Debug/ /t:"$target" /v:minimal ./OBehave/OBehave.Tests.csproj
if [ $? = 0 ] ; then
    exit $?
fi

xbuild /p:Configuration=Release /p:OutPutPath=../build/mono-tests/Release/ /t:"$target" /v:minimal ./OBehave/OBehave.Tests.csproj
exit $?