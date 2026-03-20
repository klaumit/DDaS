<template>
    <div class="compiler-component">
        <h1>Development</h1>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the backend has started.
        </div>

        <div v-if="post" class="content">
          <label for="ta_input">Input: </label>
          <br/>
          <textarea class="form-control" rows="12" cols="60"
                    id="ta_input" ref="ta_input"></textarea>
          <br/>

          <label for="cb_comp">Compiler: </label>
          <br/>
          <select class="form-select" id="cb_comp" ref="cb_comp">
            <option v-for="item in post" :key="item.id" :value="item.id">
              {{ item.name }} {{ item.version }} ({{ item.year }})
            </option>
          </select>
          <br/>

          <label for="ta_output">Output: </label>
          <br/>
          <textarea class="form-control" rows="12" cols="60"
                    id="ta_output" ref="ta_output"></textarea>
          <br/>

          <button type="button" class="btn"
                  @click="onCodeSubmit()">Compile!</button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    type CompilerInfo = {
        id: string,
        name: string,
        version: string,
        year: number
    }[];

    interface Data {
        loading: boolean,
        post: null | CompilerInfo
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                post: null
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
                this.post = null;
                this.loading = true;

                let response = await fetch('api/compile/ids');
                if (response.ok) {
                    this.post = await response.json();
                    this.loading = false;
                }
            },
            async uploadAndDL(file: File, kind: string, comp: string) {
                const parm = new FormData();
                parm.append('file', file);
                let response = await fetch('api/compile/'+kind+'/'+comp, {
                    method: 'POST', body: parm
                });
                return await response.text();
            },
            async onCodeSubmit() {
                let input = (<any>this.$refs.ta_input).value;
                let output = (<any>this.$refs.ta_output);
                let comp = (<any>this.$refs.cb_comp).value;
                const p = 'text/plain';
                let f = new File([input], 'mine.c', { type: p });
                output.value = await this.uploadAndDL(f, 'asm', comp);
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
