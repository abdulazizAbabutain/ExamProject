import { useEffect, useState } from 'react';
import { createTag, getTagsWithMeta } from '../api/Services/tagService';
import type { Tag } from '../models/tagsModel/Tag';
import type { MetaData } from '../models/common/PagedResponse';
import { useNavigate } from 'react-router-dom';

export default function TagList() {
  const [tags, setTags] = useState<Tag[]>([]);
  const [meta, setMeta] = useState<MetaData>();
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(50);
  const [search, setSearch] = useState('');
  const [colorCategory, setColorCategory] = useState('');
  const [isArchived, setIsArchived] = useState('');
  const [localSearch, setLocalSearch] = useState('');
  const [localColorCategory, setLocalColorCategory] = useState('');
  const [localIsArchived, setLocalIsArchived] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [newTagName, setNewTagName] = useState('');
  const [newTagColor, setNewTagColor] = useState('#000000');
  const [formError, setFormError] = useState<string[]>([]);
  const [submitting, setSubmitting] = useState(false);
  const navigate = useNavigate();

  const fetchTags = async () => {
    setLoading(true);
    const res = await getTagsWithMeta({
      PageNumber: page,
      PageSize: pageSize,
      Search: search,
      ColorCategory: colorCategory,
      IsArchived: isArchived ? isArchived === 'true' : undefined
    });
    if (res.isSuccess && res.value) {
      setTags(res.value.data);
      setMeta(res.value.metaData);
    } else {
      console.error(res.errors);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchTags();
  }, [page, pageSize, search, colorCategory, isArchived]);

  const handleNext = () => {
    if (meta?.hasNextPage) setPage((prev) => prev + 1);
  };

  const handlePrev = () => {
    if (meta?.hasPreviousPage) setPage((prev) => Math.max(prev - 1, 1));
  };

  const handlePageSizeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setPageSize(Number(e.target.value));
    setPage(1);
  };

  const handleViewDetails = (id: string) => navigate(`/tags/${id}`);
  const handleViewTimeline = (id: string) => navigate(`/tags/${id}/timeline`);

  const handleApplyFilters = () => {
    setSearch(localSearch);
    setColorCategory(localColorCategory);
    setIsArchived(localIsArchived);
    setPage(1);
  };

  const handleCreateTag = async () => {
    if (!newTagName.trim()) {
      setFormError(['Please provide a valid name.']);
      return;
    }
    setSubmitting(true);
    setFormError([]);

    try {
      const result = await createTag(newTagName, newTagColor);
      if (!result.isSuccess) {
        setFormError(result.errors);
        setSubmitting(false);
        return;
      }
      if (result.isSuccess && result.value) {
        setShowModal(false);
        setNewTagName(result.value?.name);
        setNewTagColor(result.value?.colorHexCode);
        await fetchTags();
      }


    } catch (err) {
      console.error(err);
      setFormError(['An unexpected error occurred.']);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div style={{ padding: '2rem', backgroundColor: 'white' }}>
      <h2>Tags</h2>

      <button onClick={() => setShowModal(true)} style={{ marginBottom: '1rem' }}>+ Add Tag</button>

      <div style={{ marginBottom: '1rem' }}>
        <label htmlFor="pageSize">Page Size: </label>
        <select id="pageSize" value={pageSize} onChange={handlePageSizeChange}>
          <option value={50}>50</option>
          <option value={100}>100</option>
          <option value={150}>150</option>
        </select>
      </div>

      <input type="text" placeholder="Search by name..." value={localSearch} onChange={(e) => setLocalSearch(e.target.value)} style={{ marginRight: '1rem', padding: '0.5rem' }} />
      <select value={localColorCategory} onChange={(e) => setLocalColorCategory(e.target.value)} style={{ marginRight: '1rem' }}>
        <option value="">All Colors</option>
        <option value="Red">Red</option>
        <option value="Green">Green</option>
        <option value="Blue">Blue</option>
        <option value="Yellow">Yellow</option>
        <option value="Orange">Orange</option>
        <option value="Purple">Purple</option>
        <option value="Cyan">Cyan</option>
        <option value="Pink">Pink</option>
      </select>
      <select value={localIsArchived} onChange={(e) => setLocalIsArchived(e.target.value)} style={{ marginRight: '1rem' }}>
        <option value="">All</option>
        <option value="false">Active</option>
        <option value="true">Archived</option>
      </select>
      <button onClick={handleApplyFilters}>Apply</button>

      {loading ? (
        <p>Loading...</p>
      ) : tags.length === 0 ? (
        <p>No tags found.</p>
      ) : (
        <>
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr>
                <th style={thStyle}>#</th>
                <th style={thStyle}>Name</th>
                <th style={thStyle}>Color</th>
                <th style={thStyle}>Preview</th>
                <th style={thStyle}>Actions</th>
              </tr>
            </thead>
            <tbody>
              {tags.map((tag, index) => (
                <tr key={tag.id}>
                  <td style={tdStyle}>{(page - 1) * pageSize + index + 1}</td>
                  <td style={tdStyle}>{tag.name}</td>
                  <td style={tdStyle}>{tag.colorCategory}</td>
                  <td style={tdStyle}>
                    <span style={{ backgroundColor: tag.colorCode, padding: '0.3rem 1rem', borderRadius: '5px', color: '#fff', display: 'inline-block' }}>{tag.name}</span>
                  </td>
                  <td style={tdStyle}>
                    <button onClick={() => handleViewDetails(tag.id)}>Details</button>
                    <button onClick={() => handleViewTimeline(tag.id)}>TimeLine</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <div style={{ marginTop: '1rem' }}>
            <button onClick={handlePrev} disabled={!meta?.hasPreviousPage}>Previous</button>
            <span style={{ margin: '0 1rem' }}>Page {page}</span>
            <button onClick={handleNext} disabled={!meta?.hasNextPage}>Next</button>
          </div>
        </>
      )}

      {showModal && (
        <div style={modalOverlayStyle}>
          <div style={modalStyle}>
            <h3>Add New Tag</h3>
            {formError.length > 0 && (
              <ul style={{ color: 'red' }}>
                {formError.map((err, i) => (
                  <li key={i}>{err}</li>
                ))}
              </ul>
            )}
            <input type="text" placeholder="Tag name" value={newTagName} onChange={(e) => setNewTagName(e.target.value)} style={{ marginBottom: '0.5rem', width: '100%', padding: '0.5rem' }} />
            <div style={{ display: 'flex', alignItems: 'center', gap: '1rem', marginBottom: '1rem' }}>
              <label htmlFor="colorPicker">Pick Color:</label>
              <input
                id="colorPicker"
                type="color"
                value={newTagColor}
                onChange={(e) => setNewTagColor(e.target.value)}
                style={{ width: '50px', height: '30px', border: 'none', background: 'none' }}
              />
              <span>{newTagColor}</span>
            </div>
            <div>
              <button onClick={handleCreateTag} disabled={submitting}>{submitting ? 'Submitting...' : 'Submit'}</button>
              <button onClick={() => setShowModal(false)} style={{ marginLeft: '1rem' }}>Cancel</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

const thStyle: React.CSSProperties = {
  border: '1px solid #ccc',
  padding: '0.5rem',
  textAlign: 'left'
};

const tdStyle: React.CSSProperties = {
  border: '1px solid #ddd',
  padding: '0.5rem'
};

const modalOverlayStyle: React.CSSProperties = {
  position: 'fixed',
  top: 0,
  left: 0,
  width: '100vw',
  height: '100vh',
  backgroundColor: 'rgba(0, 0, 0, 0.5)',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  zIndex: 999
};

const modalStyle: React.CSSProperties = {
  backgroundColor: 'white',
  padding: '2rem',
  borderRadius: '8px',
  width: '400px',
  boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)'
};
