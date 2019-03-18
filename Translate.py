# pip3 install py_translator

from py_translator import Translator

text = Translator().translate(text='Hello World', dest='es').text
print(text)
