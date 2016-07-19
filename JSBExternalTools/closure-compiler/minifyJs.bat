@echo off
for %%f IN (%*) DO (
	call :minify %%f
)
goto :eof

:minify
set input=%1
set output=%input:.bytes=.min.bytes%
echo inputFile: %input%
echo outputFile: %output%
echo.
java -Xms256m -Xmx1024m -jar JSBExternalTools\closure-compiler\compiler.jar --js %input% --js_output_file %output% 2>&1
goto :eof