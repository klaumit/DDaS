FROM ubuntu:noble

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    ca-certificates

COPY ./tkchia.sources /etc/apt/sources.list.d/tkchia-ubuntu-build-ia16-noble.sources
COPY ./dosemu.sources /etc/apt/sources.list.d/dosemu2-ubuntu-ppa-noble.sources

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    binutils-ia16-elf gcc-ia16-elf libi86-ia16-elf libnewlib-ia16-elf \
    dosemu2 nasm tree file nano locales \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -ms /bin/bash appusr
ADD  --chown=appusr:appusr ./dos/BCPP31.tar.gz /dos/
COPY --chown=appusr:appusr ./output/ /app/
COPY --chown=appusr:appusr ./dosemurc /dos/
COPY --chown=appusr:appusr ./startd.bat /dos/

RUN echo "LC_ALL=en_US.UTF-8" >> /etc/environment && \
    echo "en_US.UTF-8 UTF-8" >> /etc/locale.gen && \
    echo "LANG=en_US.UTF-8" > /etc/locale.conf && \
    locale-gen en_US.UTF-8

ENV LANGUAGE=en_US.UTF-8
ENV LANG=en_US.UTF-8
ENV LC_ALL=en_US.UTF-8

ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
USER appusr
RUN mkdir -p /home/appusr/.dosemu/drives && \
    ln -s /dos /home/appusr/.dosemu/drive_c && \
    mkdir -p /app/tmp && \
    mv /dos/startd.bat /app/tmp && \
    ln -s /app/tmp /home/appusr/.dosemu/drives/drive_d && \
    mv /dos/dosemurc /home/appusr/.dosemurc    
ENTRYPOINT [ "./DDaS.Server" ]

