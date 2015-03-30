FROM ubuntu:14.04
MAINTAINER Andre Taddeini "ataddeini@gmail.com"

RUN apt-get update
RUN apt-get -y -q install build-essential nodejs npm

ADD package.json /tmp/package.json
RUN cd /tmp && npm install
RUN mkdir -p /opt/recipes && cp -a /tmp/node_modules /opt/recipes

COPY bin /opt/recipes/bin
COPY public /opt/recipes/public
COPY routes /opt/recipes/routes
COPY views /opt/recipes/views
COPY app.js /opt/recipes/app.js

EXPOSE 3000

WORKDIR /opt/recipes
CMD ["nodejs", "./bin/www"]
