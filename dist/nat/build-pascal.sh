#!/bin/sh

#            http://mirror.freemirror.org/pub/fpc/dist/3.2.2/x86_64-linux/fpc-3.2.2.x86_64-linux.tar
FPC_TAR_URL="https://downloads.freepascal.org/fpc/dist/3.2.2/x86_64-linux/fpc-3.2.2.x86_64-linux.tar"
FPC_TAR="fpc-linux.tar"

#            http://mirror.freemirror.org/pub/fpc/dist/3.2.2/i8086-msdos/fpc-3.2.2.x86_64-linux.cross.i8086-msdos.tar.xz
FPD_TAR_URL="https://downloads.freepascal.org/fpc/dist/3.2.2/i8086-msdos/fpc-3.2.2.x86_64-linux.cross.i8086-msdos.tar.xz"
FPD_TAR="fpc-msdos.tar.xz"

if [ ! -f "$FPC_TAR" ]; then
    curl -o "$FPC_TAR" "$FPC_TAR_URL"
fi

if [ ! -f "$FPD_TAR" ]; then
    curl -o "$FPD_TAR" "$FPD_TAR_URL"
fi

FPC_MSU="lib/fpc/3.2.2/units/msdos"
FPC_MSD="./usr/$FPC_MSU"
FPC_MSF="fpc-msdos-o.tar.gz"

if [ ! -f "$FPC_MSF" ]; then
    unxz -k $FPD_TAR
    mkdir ./usr
    cd ./usr
    tar -xf ../fpc-msdos.tar
    rm ../fpc-msdos.tar
    rm -R ./$FPC_MSU/8086-compact
    rm -R ./$FPC_MSU/8086-huge
    rm -R ./$FPC_MSU/8086-large
    rm -R ./$FPC_MSU/8086-medium
    rm -R ./$FPC_MSU/8086-small
    rm -R ./$FPC_MSU/80186-compact
    rm -R ./$FPC_MSU/80186-huge
    rm -R ./$FPC_MSU/80186-large
    rm -R ./$FPC_MSU/80186-medium
    rm -R ./$FPC_MSU/80186-small
    rm -R ./$FPC_MSU/80286-compact
    rm -R ./$FPC_MSU/80286-huge
    rm -R ./$FPC_MSU/80286-large
    rm -R ./$FPC_MSU/80286-medium
    rm -R ./$FPC_MSU/80286-small
    rm -R ./$FPC_MSU/80286-tiny
    rm -R ./$FPC_MSU/80386-compact
    rm -R ./$FPC_MSU/80386-huge
    rm -R ./$FPC_MSU/80386-large
    rm -R ./$FPC_MSU/80386-medium
    rm -R ./$FPC_MSU/80386-small
    rm -R ./$FPC_MSU/80386-tiny
    cd ..
    tar -czf $FPC_MSF usr
    rm -R ./usr
fi

FPC_BSE="fpc-3.2.2.x86_64-linux"
FPC_BSD="./setup/$FPC_BSE"
FPC_BSF="fpc-linux-o.tar.gz"

if [ ! -f "$FPC_BSF" ]; then
    mkdir ./setup
    cd ./setup
    tar -xf ../$FPC_TAR
    rm ./$FPC_BSE/demo.tar.gz
    rm ./$FPC_BSE/doc-pdf.tar.gz
    mkdir ./$FPC_BSE/tmp
    cd ./$FPC_BSE/tmp
    tar xf ../binary.x86_64-linux.tar
    rm units-*.tar.gz
    rm utils-p*.tar.gz
    rm utils-l*.tar.gz
    rm utils-j*.tar.gz
    rm utils-h*.tar.gz
    rm utils-i*.tar.gz
    rm utils-u*.tar.gz
    rm utils-fpp*.tar.gz
    rm utils-fpd*.tar.gz
    rm utils-fpr*.tar.gz
    rm utils-fpcr*.tar.gz
    cd ..
    (cd ./tmp && tar -cf ../binary.min.tar *)
    rm -R ./tmp
    mv binary.min.tar binary.x86_64-linux.tar
    sed -i 's|ask "Install prefix (/usr or /usr/local) " PREFIX|PREFIX=/usr|' ./install.sh
    cd ..
    mv $FPC_BSE fpc
    cd ..
    tar -czf $FPC_BSF setup
    rm -R ./setup    
fi


