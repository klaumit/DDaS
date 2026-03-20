<template>
    <div class="compiler-component">
        <h1>Compile</h1>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the backend has started.
        </div>

        <div v-if="post" class="content">
          <label for="ta_input">Input: </label>
          <br/>
          <textarea class="form-control" rows="10" id="ta_input"></textarea>
          <br/>

          <label for="cb_comp">Compiler: </label>
          <br/>
          <select class="form-select" id="cb_comp">
            <option v-for="item in post" :key="item.id" value="{{ item.id }}">
              {{ item.name }} {{ item.version }} ({{ item.year }})
            </option>
          </select>
          <br/>

          <label for="ta_output">Output: </label>
          <br/>
          <textarea class="form-control" rows="10" id="ta_output"></textarea>
          <br/>

          <button type="button" class="btn"
                  onclick="onCodeSubmit()">Execute!</button>
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
            onCodeSubmit()
            {
                var input = $('#ta_input').val().trim();
                var output = $('#ta_output').val().trim();
                var comp = $('#cb_comp').val();

                alert(input+" | "+output+" | "+comp);
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
