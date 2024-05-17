import { nodeResolve } from "@rollup/plugin-node-resolve";
import { defineConfig } from "vite";
/** @type {import('vite').UserConfig} */
export default defineConfig({
  root: ".",
  build: {
    outDir: "dist",
    rollupOptions: "",
  },
  plugins: [
    nodeResolve({
      // Add this line for development config, omit for production config
      exportConditions: ["development"],
    }),
  ],
});
