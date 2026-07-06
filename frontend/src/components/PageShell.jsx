import '../app/App.css'

export default function PageShell({ children, className = '' }) {
  return <main className={`app-shell ${className}`.trim()}>{children}</main>
}
