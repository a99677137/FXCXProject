#!/bin/bash

SHELL_FOLDER=$(cd "$(dirname "$0")";pwd)
echo $SHELL_FOLDER;

FBS_PATH=${SHELL_FOLDER}"/Generate/Fbs"
JSON_PATH=${SHELL_FOLDER}"/Generate/Json"
echo $FBS_PATH
echo $JSON_PATH

${SHELL_FOLDER}/flatc -n -o ${SHELL_FOLDER}/cs $FBS_PATH $JSON_PATH
${SHELL_FOLDER}/flatc -b -o ${SHELL_FOLDER}/bin/Table $FBS_PATH $JSON_PATH