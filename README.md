# wikix

A Wikipedia XML database dump file parser console application.


Tested with `enwiki-20170420-pages-articles.xml 61.676.230.130 bytes (57,4 GB)`

## Usage

`wikix <full-path-to-source-xml-filename> <full-path-to-json-export-file> <c#-regular-expression-1 *> <c#-regular-expression-2>`  

* https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

## Example

`wikix c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.xml c:\\git\\wiki\\data-dumps\\enwiki-20170420-pages-articles.json (\(name\))$  (\(surname\))$ "surnames|given names"


```bash
Processing enwiki-20170420-pages-articles.xml to write data to enwiki-20170420-pages-articles.json
Stay tuned. This could take a few minutes...
Crawled 100.000 and found 21 pages...
Crawled 200.000 and found 54 pages...
Crawled 300.000 and found 88 pages...
100 pages found. Currently "Banu (name)" at #329.495
...
Crawled 17.400.000 and found 14.963 pages...
15.000 pages found. Currently "Vytenis (name)" at #17.446.694
flushing file contents...
Crawled 17.483.909 pages. done.
RunTime 00:08:01.83
```