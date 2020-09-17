#!/usr/bin/env bash
aws ssm get-parameters --region eu-west-1 --name thimble-main-apiKey thimble-main-accessKey thimble-main-secretKey > secrets.json
python3 ./tools/jenkins/add-secrets.py