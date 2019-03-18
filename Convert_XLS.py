# Converts txt to xls
# space = right align, tab = next column, next line = next row

from pathlib import Path

filename = "Twilio.txt"
new_filename = Path(filename).stem + ".xls"
