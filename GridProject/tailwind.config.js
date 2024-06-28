/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"
  ],
  theme: {
    extend: {
      colors: {
        'primary-color': 'var(--primary-color)',
        'color-danger': 'var(--color-danger)',
        'title-color': 'var(--title-color)',
        'sub-title-color': 'var(--sub-title-color)',
        'border-color': 'var(--border-color)',
        'place-holder-color': 'var(--place-holder-color)',
        'border-light-color':'var(--border-light-color)'
      },
      minHeight: {
        'minHeight-layout-web': 'calc(100vh - 200px)',
        'minHeight-login-web': 'calc(100vh - 130px)',

      },
    },
  },
  plugins: [],
}

