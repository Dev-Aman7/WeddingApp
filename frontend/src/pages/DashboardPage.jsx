import { useMemo } from 'react'
import { useSelector } from 'react-redux'
import { selectToken } from '../features/auth/authSlice'
import { useGetGuestsQuery } from '../features/api/apiSlice'
import PageShell from '../components/PageShell'

export default function DashboardPage() {
  const token = useSelector(selectToken)
  const { data: guests = [], isLoading, isError } = useGetGuestsQuery(undefined, {
    skip: !token,
  })

  const summary = useMemo(() => {
    if (!guests.length) {
      return { title: 'No guests yet', description: 'Add guests from the API to see them here.' }
    }

    return {
      title: `${guests.length} guests tracked`,
      description: 'Your guest list is ready to review and manage.',
    }
  }, [guests.length])

  return (
    <PageShell>
      <section className="dashboard-card">
        <p className="eyebrow">Wedding dashboard</p>
        <h1>{summary.title}</h1>
        <p>{summary.description}</p>

        {isLoading ? <p className="status-pill">Loading guests...</p> : null}
        {isError ? <p className="status-pill status-pill--error">Unable to load guest data.</p> : null}

        {!isLoading && !isError && guests.length > 0 ? (
          <ul className="guest-list">
            {guests.map((guest) => (
              <li key={guest.id ?? guest.name} className="guest-list__item">
                <strong>{guest.name}</strong>
                <span>{guest.email ?? 'No email provided'}</span>
              </li>
            ))}
          </ul>
        ) : null}
      </section>
    </PageShell>
  )
}
