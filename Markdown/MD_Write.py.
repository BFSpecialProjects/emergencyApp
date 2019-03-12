import markdown
import codecs

md = markdown.Markdown()

output_file = codecs.open("MD_Write_Ex.html", "w",
                          encoding="utf-8",
                          errors="xmlcharrefreplace")

text = "# Sample Text Heading \n  " \
       "Next Line  \n" \
       "**This is Bold**  \n" \
       "*This is Italicized*  \n" \
       "1. This is a list  \n" \
       "2. This is the second item in the list  \n" \
       "- This is an unordered list  \n" \
       "- This is the second item in the unordered list  \n" \
       "[This is a link](www.example.com)  \n"
html = md.convert(text)

output_file.write(html)
