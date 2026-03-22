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
        text: null | string
    };

    type ToolInfo = {
        id: string,
        name: string,
        version: string,
        year: number
    }[];

    interface Data {
        loading: boolean,
        postC: null | ToolInfo,
        postA: null | ToolInfo
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
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
            async uploadAndDL(file: File, meth: string, kind: string, comp: string) : Promise<Executed> {
                const parm = new FormData();
                parm.append('file', file);
                let response = await fetch('api/'+meth+'/'+kind+'/'+comp, {
                    method: 'POST', body: parm
                });
                let res : Executed = { exit: null, milli: null,
                                       output: null, status: null, text: null };
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
                res.text = await response.text();
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
                output.value = u.text;
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
                let u = await this.uploadAndDL(f,'assemble','com',comp);
                output.value = u.text;
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
