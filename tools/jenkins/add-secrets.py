#!/usr/bin/python3

import json

secrets ={}
data = {}

with open("./src/Thimble.Credentials/appsettings.json", "r") as f:
    data = json.load(f)

with open("secrets.json", "r") as f:
    secrets = json.load(f)
    for param in secrets["Parameters"]:
        if "apiKey" in param["Name"]:
            data["apiKey"] =  param["Value"]
        if "accessKey" in param["Name"]:
            data["awsAccessKey"] =  param["Value"]
        if "secretKey" in param["Name"]:
            data["awsSecretKey"] =  param["Value"]

with open('./src/Thimble.Credentials/appsettings.json', 'w') as f:
    json.dump(data, f)