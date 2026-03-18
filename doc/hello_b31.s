	.286
	ifndef	??version
?debug	macro
	endm
publicdll macro	name
	public	name
	endm
$comm	macro	name,dist,size,count
	comm	dist name:BYTE:count*size
	endm
	else
$comm	macro	name,dist,size,count
	comm	dist name[size]:BYTE:count
	endm
	endif
	?debug	V 300h
	?debug	S "hello.c"
	?debug	C E95102725C0768656C6C6F2E63
	?debug	C E91146715C19433A5C4243505033315C494E434C5544455C737464+
	?debug	C 696F2E68
	?debug	C E91146715C19433A5C4243505033315C494E434C5544455C5F6465+
	?debug	C 66732E68
	?debug	C E91146715C1A433A5C4243505033315C494E434C5544455C5F6E66+
	?debug	C 696C652E68
	?debug	C E91146715C19433A5C4243505033315C494E434C5544455C5F6E75+
	?debug	C 6C6C2E68
	?debug	C E91146715C1A433A5C4243505033315C494E434C5544455C737464+
	?debug	C 6C69622E68
_TEXT	segment byte public 'CODE'
_TEXT	ends
DGROUP	group	_DATA,_BSS
	assume	cs:_TEXT,ds:DGROUP
_DATA	segment word public 'DATA'
d@	label	byte
d@w	label	word
_DATA	ends
_BSS	segment word public 'BSS'
b@	label	byte
b@w	label	word
_BSS	ends
_TEXT	segment byte public 'CODE'
   ;	
   ;	int main(int argc, char **argv)
   ;	
	assume	cs:_TEXT
_main	proc	near
	push	bp
	mov	bp,sp
   ;	
   ;	{
   ;	       printf("Hello world\n");
   ;	
	push	offset DGROUP:s@
	call	near ptr _printf
	pop	cx
   ;	
   ;	       exit(0);
   ;	
	push	0
	call	near ptr _exit
	pop	cx
   ;	
   ;	}
   ;	
	pop	bp
	ret	
_main	endp
	?debug	C E9
	?debug	C FA00000000
_TEXT	ends
_DATA	segment word public 'DATA'
s@	label	byte
	db	'Hello world'
	db	10
	db	0
_DATA	ends
_TEXT	segment byte public 'CODE'
_TEXT	ends
	extrn	__setargv__:far
	public	_main
	extrn	_exit:near
	extrn	_printf:near
_s@	equ	s@
	end
