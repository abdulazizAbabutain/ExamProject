import type { TagFormModel } from "@/models/tags"
import React, { useState } from "react";


type Prop = {
    onSubmit: (tag: Partial<TagFormModel>) => void;
    initial: Partial<TagFormModel>;
    isLoading: boolean;
}


export default function TagForm({ onSubmit, initial = {}, isLoading }: Prop) {
    const [Name, setName] = useState(initial.Name || '');
    const [ColorCode, setColorCode] = useState(initial.ColorCode || '');

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit({ Name, ColorCode });
    };


    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Name :</label>
                <input required value={Name} onChange={(e) => setName(e.target.value)} />
            </div>
            <div style={{ display: 'flex', alignItems: 'center', gap: '1rem', marginBottom: '1rem' }}>
                <label htmlFor="colorPicker">Pick Color:</label>
                <input
                    id="colorPicker"
                    type="color"
                    value={ColorCode}
                    onChange={(e) => setColorCode(e.target.value)}
                    style={{ width: '50px', height: '30px', border: 'none', background: 'none' }}
                />
                <span>{ColorCode}</span>
            </div>
            
            <button type="submit" disabled={isLoading}>
                {isLoading ? 'Creating...' : 'Create Tag'}
            </button>
        </form>

    )

}