#!/bin/bash

function getVersion {
input="Cresce.Api/appsettings.json"
while IFS= read -r line
do
  if [[ $line == *"Version"* ]]; then
    echo "$line"
    return
  fi

done < "$input"
}

function increment_version
{
  declare -a part=( ${1//\./ } )
  declare    new
  declare -i carry=1

  for (( CNTR=${#part[@]}-1; CNTR>=0; CNTR-=1 )); do
    len=${#part[CNTR]}
    new=$((part[CNTR]+carry))
    [ ${#new} -gt $len ] && carry=1 || carry=0
    [ $CNTR -gt 0 ] && part[CNTR]=${new: -len} || part[CNTR]=${new}
  done
  new="${part[*]}"
  echo -e "${new// /.}"
}

version=$(echo $(getVersion) | sed 's/"Version": "//g' | sed 's/",//g')


newVersion=$(echo $(increment_version $version))

echo "$version > $newVersion"

sed "s/$version/$newVersion/" Cresce.Api/appsettings.json > changed.txt && mv changed.txt Cresce.Api/appsettings.json
