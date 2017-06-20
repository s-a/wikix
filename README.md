# wikix

A Wikipedia XML database dump file parser.


Tested with `enwiki-20170420-pages-articles.xml 61.676.230.130 bytes (57,4 GB)`

## Usage

`wikix <full-path-to-source-xml-filename> <full-path-to-json-export-file> <c#-regular-expression-1 *> <c#-regular-expression-2>`  

* https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

## Example

`wikix c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.xml c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.json (\(name\))$  (\(surname\))$ "surnames|given names"