<template>
    <div class="weather-component">
        <h1>Compile</h1>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the backend has started.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Version</th>
                        <th>Year</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in post" :key="item.id">
                        <td>{{ item.id }}</td>
                        <td>{{ item.name }}</td>
                        <td>{{ item.version }}</td>
                        <td>{{ item.year }}</td>
                    </tr>
                </tbody>
            </table>
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

                var response = await fetch('api/compile/ids');
                if (response.ok) {
                    this.post = await response.json();
                    this.loading = false;
                }
            }
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

.weather-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>
