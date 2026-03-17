FROM ubuntu:noble

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    ca-certificates
    
COPY ./tkchia.sources /etc/apt/sources.list.d/tkchia-ubuntu-build-ia16-noble.sources

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    binutils-ia16-elf gcc-ia16-elf libi86-ia16-elf libnewlib-ia16-elf \
    nasm file nano \
    && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY ./output/ /app/
ENTRYPOINT [ "./DDaS.Server" ]

