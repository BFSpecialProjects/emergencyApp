import boto3 # AWS
from py_translator import Translator # Google Translate 

# Google Translate
text = Translator().translate(text='Hello World', dest='es').text
print(text)

# AWS
# Requires AWS CLI - ref https://docs.aws.amazon.com/translate/latest/dg/setup-awscli.html
translate = boto3.client(service_name='translate', region_name='region', use_ssl=True)

result = translate.translate_text(Text="Hello, World", 
            SourceLanguageCode="en", TargetLanguageCode="de")
print('TranslatedText: ' + result.get('TranslatedText'))
print('SourceLanguageCode: ' + result.get('SourceLanguageCode'))
print('TargetLanguageCode: ' + result.get('TargetLanguageCode'))
