bits 16

section _STARTUP use16 progbits alloc class=CODE
section _TEXT use16 progbits alloc class=CODE
section _DATA use16 progbits alloc class=DATA
section _BSS use16 nobits alloc class=BSS

group DGROUP _STARTUP _TEXT _DATA _BSS

extern main_
global _s_

section _STARTUP

resb 100h

_s_:
    call main_

    mov ax, 4c00h
    int 21h

