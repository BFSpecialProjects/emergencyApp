import markdown
import codecs

input_file = codecs.open("MD_Read_Ex.txt", mode="r", encoding="utf-8")
text = input_file.read()
html = markdown.markdown(text)
print(text)
