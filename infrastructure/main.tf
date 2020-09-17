terraform {
  backend "s3" {
    region  = "eu-west-1"
    bucket  = "thimble-terraform-state"
    key     = "credentials/terraform.tfstate"        
  }
}  

provider "aws" {
  region = "eu-west-1"
}

data "aws_caller_identity" "current" {}

module "vpc" {
  source = "./vpc"
  service_name="credentials"
}


