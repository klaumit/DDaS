<template>
    <div class="compiler-component">
        <h1>Development</h1>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the backend has started.
        </div>

        <div v-if="postA && postC" class="content">
          <label for="ta_input">Input: </label>
          <br/>
          <textarea class="form-control" rows="12" cols="60"
                    id="ta_input" ref="ta_input"></textarea>
          <br/>

          <label for="cb_comp">Compiler: </label>
          <br/>
          <select class="form-select" id="cb_comp" ref="cb_comp">
            <option v-for="item in postC" :key="item.id" :value="item.id">
              {{ item.name }} {{ item.version }} ({{ item.year }})
            </option>
          </select>
          <br/>

          <label for="cb_asse">Assembler: </label>
          <br/>
          <select class="form-select" id="cb_asse" ref="cb_asse">
            <option v-for="item in postA" :key="item.id" :value="item.id">
              {{ item.name }} {{ item.version }} ({{ item.year }})
            </option>
          </select>
          <br/>

          <label for="ta_output">Output: </label>
          <br/>
          <textarea class="form-control" rows="12" cols="60"
                    id="ta_output" ref="ta_output"></textarea>
          <VueHex v-if="byteD" v-model="byteD" />
          <br/>

          <label for="ta_dbglog">Log: </label>
          <br/>
          <textarea class="form-control" rows="5" cols="60"
                    id="ta_dbglog" ref="ta_dbglog"></textarea>
          <br/>

          <button type="button" class="btn"
                  @click="onCodeCompile()">Compile!</button>
          <button type="button" class="btn"
                  @click="onCodeAssemble()">Assemble!</button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    type Executed = {
        exit: null | number,
        milli: null | number,
        output: null | string,
        status: null | number,
        txt: null | string,
        raw: null | Uint8Array<ArrayBuffer>
    };

    type ToolInfo = {
        id: string,
        name: string,
        version: string,
        year: number
    }[];

    interface Data {
        loading: boolean,
        byteD: null | Uint8Array<ArrayBuffer>,
        postC: null | ToolInfo,
        postA: null | ToolInfo
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                byteD: null,
                postC: null,
                postA: null
            };
        },
        async created() {
            await this.fetchData();
            let tia = <any>this.$refs.ta_input;
            tia.value = '#include <stdio.h>\n' +
              '#include <stdlib.h>\n' +
              '\n' +
              'int main(int argc, char **argv)\n' +
              '{\n' +
              '       printf("Hello world\\n");\n' +
              '       exit(0);\n' +
              '}\n';
            tia.value = 'org 100h\n'+
                '\n'+
                'start:\n'+
                '    mov  dx, msg\n'+
                '    mov  ah, 09h\n'+
                '    int  21h\n'+
                '\n'+
                '    mov  ax, 4C00h\n'+
                '    int  21h\n'+
                '\n'+
                'msg db \'Hello world\', 13, 10, \'$\'\n'+
                '\n';
            let toa = <any>this.$refs.ta_output;
            toa.value = 'hello\nworld\nshit';
            let tla = <any>this.$refs.ta_dbglog;
            tla.value = 'kack\nyou\nor\nwhat';
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            async fetchData() {
                this.postC = null;
                this.postA = null;
                this.loading = true;

                let responseC = await fetch('api/compile/ids');
                if (responseC.ok) {
                    this.postC = await responseC.json();
                    this.loading = false;
                }
                let responseA = await fetch('api/assemble/ids');
                if (responseA.ok) {
                    this.postA = await responseA.json();
                    this.loading = false;
                }
            },
            async uploadAndDL(file: File, meth: string, kind: string, comp: string,
                              raw: boolean = false) : Promise<Executed> {
                const parm = new FormData();
                parm.append('file', file);
                let response = await fetch('api/'+meth+'/'+kind+'/'+comp, {
                    method: 'POST', body: parm
                });
                let res : Executed = { exit: null, milli: null, raw: null,
                                       output: null, status: null, txt: null };
                let drt = response.headers.get('x-ddas-ret')?.split(' ; ');
                if (drt && drt[0] && drt[1])
                {
                    res.exit = Number.parseInt(drt[0]);
                    res.milli = Number.parseInt(drt[1]);
                }
                let dot = response.headers.get('x-ddas-out');
                if (dot)
                {
                    res.output = atob(dot);
                }
                res.status = response.status;
                if (raw)
                    res.raw = await response.bytes();
                else
                    res.txt = await response.text();
                return res;
            },
            async onCodeCompile() {
                let input = (<any>this.$refs.ta_input).value;
                let output = (<any>this.$refs.ta_output);
                let outlog = (<any>this.$refs.ta_dbglog);
                let comp = (<any>this.$refs.cb_comp).value;
                const p = 'text/plain';
                let f = new File([input], 'mine.c', { type: p });
                let u = await this.uploadAndDL(f,'compile','asm',comp);
                output.value = u.txt;
                this.byteD = null;
                outlog.value = '{ exit_code = '+u.exit+', runtime_ms = '+u.milli+', status = '+u.status+' }';
                outlog.value += '\n'+u.output;
            },
            async onCodeAssemble() {
                let input = (<any>this.$refs.ta_input).value;
                let output = (<any>this.$refs.ta_output);
                let outlog = (<any>this.$refs.ta_dbglog);
                let comp = (<any>this.$refs.cb_asse).value;
                const p = 'text/plain';
                let f = new File([input], 'mine.asm', { type: p });
                let u = await this.uploadAndDL(f,'assemble','com',comp,true);
                output.value = null;
                this.byteD = u.raw;
                outlog.value = '{ exit_code = '+u.exit+', runtime_ms = '+u.milli+', status = '+u.status+' }';
                outlog.value += '\n'+u.output;
            },
        },
    });
</script>

<style scoped>
th {
    font-weight: bold;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.compiler-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>
