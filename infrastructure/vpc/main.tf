variable "service_name" {
}

resource "aws_vpc" "vpc" {
  cidr_block = "10.0.0.0/16"
  instance_tenancy = "default"
  enable_dns_support = true
  enable_dns_hostnames = true

  tags = {
    Name = "${var.service_name}-vpc"
  }
}

resource "aws_subnet" "subnetone" {
    vpc_id = "${aws_vpc.vpc.id}"
    availability_zone = "eu-west-1a"
    cidr_block = "10.0.0.0/20"
    map_public_ip_on_launch = true

    tags = {
      Name = "${var.service_name}-subnet-a"
    }
}

resource "aws_subnet" "subnettwo" {
    vpc_id = "${aws_vpc.vpc.id}"
    availability_zone = "eu-west-1b"
    cidr_block = "10.0.32.0/20"
    map_public_ip_on_launch = true

    tags = {
      Name = "${var.service_name}-subnet-b"
    }
}

resource "aws_internet_gateway" "gw" {
    vpc_id = "${aws_vpc.vpc.id}"

    tags = {
      Name = "${var.service_name}-gateway"
    }
}

resource "aws_route_table" "r" {
  vpc_id         = "${aws_vpc.vpc.id}"

  tags = {
    Name = "${var.service_name}-route-table"
  }
}

resource "aws_route_table_association" "a" {
  subnet_id      = "${aws_subnet.subnetone.id}"
  route_table_id = "${aws_route_table.r.id}"
}

resource "aws_route_table_association" "b" {
  subnet_id      = "${aws_subnet.subnettwo.id}"
  route_table_id = "${aws_route_table.r.id}"
}

resource "aws_route" "awr" {
  gateway_id = "${aws_internet_gateway.gw.id}"
  route_table_id = "${aws_route_table.r.id}"
  destination_cidr_block = "0.0.0.0/0"
}

module "ecs" {
  source = "../ecs"
  service_name = "${var.service_name}"
  vpc_id = "${aws_vpc.vpc.id}"
  Subnet_Id_A = "${aws_subnet.subnetone.id}"
  Subnet_Id_B = "${aws_subnet.subnettwo.id}"
}
