#!/bin/bash
minify()
{
	input=$1
	output=${input/.bytes/.min.bytes}
	echo "inputFile:" $input
	echo "outputFile:" $output
	echo
	java -Xms256m -Xmx1024m -jar ./JSBExternalTools/closure-compiler/compiler.jar --js $input --js_output_file $output 2>&1
}

for file in $@; do
	minify $file
done