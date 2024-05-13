import { defineConfig } from "cypress";

export default defineConfig({
  e2e: {
    experimentalRunAllSpecs: true,
    baseUrl: "http://localhost:5173",
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },

  component: {
    devServer: {
        framework: 'react',
        bundler: 'vite',
    },
    specPattern: 'test/**/*.test.tsx',
    viewportWidth: 1280,
    viewportHeight: 768,
  },
});
